using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace SmartMRTDashService_Combine
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly MRT_DashRepository _repository;
        private readonly ILogger<ReportController> _logger;

        public ReportController(
            MRT_DashRepository repository,
            ILogger<ReportController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("pdf")]
        public async Task<IActionResult> GeneratePdf()
        {
            try
            {
                string globalConn =
                    MRT_ConnectionMain.GetDashboardConnection();

                var records =
                    await _repository.GetLineConfigsAsync(globalConn);

                var lines = records
                    .GroupBy(x => x.LineId)
                    .Select(g => new LineConfig
                    {
                        LineId = g.Key,
                        LineNum = g.First().LineNum,
                        MasterIpAddress = g.First().MasterIpAddress,
                        LocalIpAddress = g.First().LocalIpAddress,
                        StatusActive = g.First().StatusActive,

                        Modules = g
                            .Where(x => !string.IsNullOrEmpty(x.ModuleCode))
                            .Select(x => x.ModuleCode)
                            .Distinct()
                            .ToList()
                    })
                    .ToList();

                var report =
                    await _repository.BuildProductionReport(lines);

                var logoPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Assets",
                    "smartmrt.png");

                if (!System.IO.File.Exists(logoPath))
                    FolderLog.Error($"PDF logo not found: {logoPath}");

                byte[] pdfBytes = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(30);

                        // HEADER
                        page.Header()
                            .Row(row =>
                            {
                                row.ConstantItem(100).Element(e =>
                                {
                                    if (System.IO.File.Exists(logoPath))
                                        e.Image(logoPath);
                                });

                                row.RelativeItem()
                                    .Column(column =>
                                    {
                                        column.Item()
                                            .AlignRight()
                                            .Text("SMARTMRT PRODUCTION REPORT")
                                            .FontSize(20)
                                            .Bold();

                                        column.Item()
                                            .AlignRight()
                                            .Text(DateTime.Now.ToString("dd MMMM yyyy"))
                                            .FontSize(10);
                                    });
                            });

                        // CONTENT
                        page.Content()
                            .PaddingTop(20)
                            .Column(column =>
                            {
                                column.Item()
                                 .PaddingTop(20)
                                 .Text("PRODUCTION SUMMARY")
                                 .FontSize(16)
                                 .Bold();

                                column.Item()
                                .PaddingTop(20)
                                 .Table(table =>
                                 {
                                     table.ColumnsDefinition(columns =>
                                     {
                                         columns.ConstantColumn(50);
                                         columns.RelativeColumn();
                                         columns.RelativeColumn();
                                         columns.RelativeColumn();
                                         columns.RelativeColumn();
                                         columns.RelativeColumn();
                                     });

                                     table.Header(header =>
                                     {
                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Line");

                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Output");

                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Target");

                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Running Target");

                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Efficiency");

                                         header.Cell()
                                             .Element(HeaderStyle)
                                             .Text("Defect");
                                     });

                                     foreach (var item in report)
                                     {
                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text(item.LineNum.ToString());

                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text(item.Output.ToString());

                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text(item.Target.ToString());

                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text(item.RunningTarget.ToString());

                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text($"{item.Efficiency:F2}%");

                                         table.Cell()
                                             .Element(CellStyle)
                                             .Text($"{item.Defect:F2}%");
                                     }
                                 });

                           });                      

                        // FOOTER
                        page.Footer()
                            .AlignCenter()
                            .Text(text =>
                            {                          
                                text.Span("Generated: ");
                                text.Span(
                                    DateTime.Now.ToString(
                                        "dd-MM-yyyy HH:mm"));
                            });
                    });
                }).GeneratePdf();

                return File(
                    pdfBytes,
                    "application/pdf",
                    "SmartMRT_Production_Report.pdf");
            }
            catch (Exception ex)
            {
                FolderLog.Error($"{DateTime.Now} PDF ERROR:\r\n{ex}\r\n");

                _logger.LogError(
                    ex,
                    "PDF generation failed");

                return StatusCode(
                    500,
                    "PDF generation failed: " + ex.Message);
            }

        }


        // ======================================================
        // QUESTPDF STYLES
        // ======================================================

        private static IContainer HeaderStyle(IContainer container)
        {
            return container
                .Background(QuestPDF.Helpers.Colors.Grey.Lighten2)
                .Border(1)
                .Padding(5)
                .AlignCenter();
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .Padding(5)
                .AlignCenter();
        }



        [HttpGet("excel")]
        public async Task<IActionResult> GenerateExcel()
        {
            try
            {
                string globalConn = MRT_ConnectionMain.GetDashboardConnection();

                var records = await _repository.GetLineConfigsAsync(globalConn);
                var lines = records
                    .GroupBy(x => x.LineId)
                    .Select(g => new LineConfig
                    {
                        LineId = g.Key,
                        LineNum = g.First().LineNum,
                        MasterIpAddress = g.First().MasterIpAddress,
                        LocalIpAddress = g.First().LocalIpAddress,
                        StatusActive = g.First().StatusActive,
                        Modules = g
                            .Where(x => !string.IsNullOrEmpty(x.ModuleCode))
                            .Select(x => x.ModuleCode)
                            .Distinct()
                            .ToList()
                    })
                    .ToList();
                var report = await _repository.BuildProductionReport(lines);

                using var workbook = new XLWorkbook();
                var sheet = workbook.Worksheets.Add("Production Report");

                var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "smartmrt.png");

                if (System.IO.File.Exists(logoPath))
                {
                    sheet.AddPicture(logoPath).MoveTo(sheet.Cell("A1")).Scale(0.5);
                }

                sheet.Cell("B5").Value = "SMARTMRT PRODUCTION REPORT";
                sheet.Cell("B5").Style.Font.Bold = true;
                sheet.Cell("B5").Style.Font.FontSize = 18;
                sheet.Cell("B6").Value = DateTime.Now.ToString("dd MMMM yyyyy");

                int headerRow = 7;
                sheet.Cell(headerRow, 1).Value = "Line";
                sheet.Cell(headerRow, 2).Value = "Output";
                sheet.Cell(headerRow, 3).Value = "Target";
                sheet.Cell(headerRow, 4).Value = "Running Target";
                sheet.Cell(headerRow, 5).Value = "Efficiency";
                sheet.Cell(headerRow, 6).Value = "Defect";

                int row = headerRow + 1;

                foreach (var item in report)
                {
                    sheet.Cell(row, 1).Value = item.LineNum;
                    sheet.Cell(row, 2).Value = item.Output;
                    sheet.Cell(row, 3).Value = item.Target;
                    sheet.Cell(row, 4).Value = item.RunningTarget;
                    sheet.Cell(row, 5).Value = $"{item.Efficiency:F2}%";
                    sheet.Cell(row, 6).Value = $"{item.Defect:F2}%";
                    row++;
                }

                sheet.Columns().AdjustToContents();
                using var stream = new MemoryStream();
                workbook.SaveAs(stream);
                var excelBytes = stream.ToArray();

                //return Ok("Excel test");
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SmartMRT_Production_Report.xlsx");
            }
            catch (Exception ex)
            {
                FolderLog.Error($"{DateTime.Now} PDF ERROR:\r\n{ex}\r\n");

                _logger.LogError(
                    ex,
                    "PDF generation failed");

                return StatusCode(
                    500,
                    "PDF generation failed: " + ex.Message);
            }
              
        }
    }
}
