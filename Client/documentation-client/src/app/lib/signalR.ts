import * as signalR from '@microsoft/signalr';
import { FileInformation } from '../lib/definitions';

const connection = new signalR.HubConnectionBuilder()
    .withUrl('http://localhost:5271/files-hub', { withCredentials: false })
    .withAutomaticReconnect()
    .build();

export const startConnection = async () => {
    if (connection.state === signalR.HubConnectionState.Disconnected) {
        await connection.start();
        console.log('SignalR connected');
    } else {
        console.log('SignalR connection is already in progress or connected.');
    }
};

export const addMessageListener = (callback: (message: FileInformation[]) => void) => {
    connection.on('FilesInformation', (response: FileInformation[]) => {
        callback(response);
    });
};