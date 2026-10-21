SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
    SmartMRT Procedure : DC_GetDataGlobal
    Version            : 1
*/

CREATE OR ALTER PROCEDURE [dbo].[DC_GetDataGlobal]
(
    @Products dbo.ProductInfoType READONLY,
    @Workers dbo.WorkerInfoType READONLY,

    @NumWorkers INT OUTPUT,
    @TotalSAM DECIMAL(18,2) OUTPUT
)
AS
BEGIN

    SET NOCOUNT ON;

    SET @TotalSAM = 0;
    SET @NumWorkers = 0;

    -- Get SAM
    SELECT 
        @TotalSAM = ISNULL(SUM(SAM_VALUE), 0)
    FROM
    (
        SELECT 
            P.MONO,
            P.PRODID,
            P.DESIGNCODE,
            SUM(D.SAM) AS SAM_VALUE
        FROM @Products P

        INNER JOIN DESIGNSEQ D
            ON P.DESIGNCODE = D.DESIGNCODE

        INNER JOIN OPERATION O
            ON D.OPCODE = O.OPCODE

        INNER JOIN @Workers W
            ON W.OPCODE = D.OPCODE
            AND W.DESIGNCODE = D.DESIGNCODE
            AND W.PRODID = P.PRODID

        WHERE O.OP_CATEGORY = 1

        GROUP BY 
            P.MONO,
            P.PRODID,
            P.DESIGNCODE
    ) X;

    -- Get workers
    SELECT 
        @NumWorkers = COUNT(DISTINCT W.EMPID)
    FROM @Workers W
    INNER JOIN OPERATION O
        ON W.OPCODE = O.OPCODE
    WHERE O.OP_CATEGORY = 1;

    SELECT DISTINCT
        W.OPCODE
    FROM @Workers W
    INNER JOIN OPERATION O
        ON W.OPCODE = O.OPCODE
    WHERE O.OP_CATEGORY = 1;

END
GO