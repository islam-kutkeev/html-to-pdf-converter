'use client'
import { ChangeEvent, useEffect, useState } from "react";
import { FileInformation } from './lib/definitions';
import * as Api from './lib/api';
import * as SignalR from './lib/signalR';
import FileList from './ui/files/list';

export default function Home() {

  const [files, setFiles] = useState<FileInformation[]>([]);
  const [newFile, setNewFile] = useState<File | null>(null);

  const handleFileChange = (event: ChangeEvent<HTMLInputElement>) => {
    if (event.target.files && event.target.files.length > 0) {
      setNewFile(event.target.files[0]);
    }
  };

  useEffect(() => {
    Api.fetchFiles().then((initFiles) => {
      setFiles(initFiles);
    });

    SignalR.startConnection();

    SignalR.addMessageListener((filesCallback) => {
      setFiles(filesCallback);
    });
  }, []);

  return (
    <main>
      <div className="flex flex-wrap p-6">
        <input type="file" onChange={handleFileChange} />
        <button className="h-10 px-6 font-semibold rounded-md bg-green-400 text-white" type="button" onClick={() => Api.uploadFile(newFile)}>
          Upload
        </button>
      </div>
      <div>
        <FileList files={files} onDelete={Api.deleteFile} onDownload={Api.downloadFile} />
      </div>
    </main>
  );
}
