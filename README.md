# HTML to PDF Converter

## Overview

This project is a web api application that allows users to convert HTML files to PDF format. The backend is built using .NET 7 with REST API and SignalR for real-time updates, while the frontend is developed using React, Next.js, and TypeScript.

## Features

- Convert HTML files to PDF format.
- Real-time updates using SignalR.
- Cross-platform compatibility.

## Prerequisites

- Node.js (v14.0 or higher)
- .NET SDK 7.0
- Visual Studio Code or any preferred code editor

## Installation

### Backend
1. In `appsettings.json` stored `WorkerPollingRateInSec` that allow you configure polling rate of a converting process

    ```json
    {
        "WorkerPollingRateInSec": 30
    }
    ```

2. In `appsettings.json` stored `UploadDirectory` where all uploaded files will be saved

    ```json
    {
        "UploadDirectory": "D:\\Projects\\Pet"
    }
    ```

3. Open the `Service` folder in Visual Studio Code or your preferred code editor.

4. Install dependencies by running:

    ```bash
    dotnet restore
    ```

5. Start the backend server:

    ```bash
    dotnet run
    ```

6. The backend server should be running at `http://localhost:5271`.

### Frontend

1. Open the `Client/documentation-client` folder in a new terminal.
2. Install dependencies by running:

    ```bash
    npm install
    ```

3. Start the frontend development server:

    ```bash
    npm run dev
    ```

4. Open your browser and visit `http://localhost:3000` to access the application.

