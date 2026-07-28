import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/hubs/MRTdashCombine")
    .withAutomaticReconnect()
    .build();

export async function startConnection() {
    if (connection.state === "Disconnected") {
        await connection.start();
    }
}

export default connection;