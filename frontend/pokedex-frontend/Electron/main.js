import { app, BrowserWindow } from "electron";
import {
    installExtension,
    REACT_DEVELOPER_TOOLS,
} from "electron-devtools-installer";

function createWindow() {
    const win = new BrowserWindow({
        width: 1200,
        height: 800,
        webPreferences: {
            nodeIntegration: false,
            contextIsolation: true,
        },
    });

    win.loadURL("http://localhost:5173");

    win.webContents.once("did-finish-load", () => {
        win.webContents.openDevTools();
    });
}

app.whenReady().then(async () => {
    try {
        await installExtension(REACT_DEVELOPER_TOOLS);
        console.log("React DevTools installed");
    } catch (error) {
        console.log("React DevTools installation failed:", error);
    }

    createWindow();

    app.on("activate", () => {
        if (BrowserWindow.getAllWindows().length === 0) {
            createWindow();
        }
    });
});

app.on("window-all-closed", () => {
    if (process.platform !== "darwin") {
        app.quit();
    }
});