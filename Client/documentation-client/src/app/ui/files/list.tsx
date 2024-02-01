import React from 'react';
import { FileInformation } from '../../lib/definitions';

type FileListProps = {
    files: FileInformation[];
    onDownload: (file: FileInformation) => void;
    onDelete: (id: string) => void;
};

const FileList: React.FC<FileListProps> = ({ files, onDownload, onDelete }) => {
    return (
        <ul>
            {files.map((file) => (
                <li key={file.id}>
                    <div className="flex font-sans w-80 relative content-center">
                        <form className="flex-auto p-6">
                            <div className="flex flex-wrap">
                                <h1 className="flex-auto text-lg font-semibold text-slate-900">
                                    {file.name}
                                </h1>
                            </div>
                            <div className="flex space-x-4 mb-6 text-sm font-medium">
                                <div className="flex-auto flex space-x-4">
                                    {
                                        file.status === 'Converted' &&
                                        < button className="h-10 px-6 font-semibold rounded-md bg-blue-400 text-white" type="button" onClick={() => onDownload(file)}>
                                            Download
                                        </button>
                                    }
                                    <button className="h-10 px-6 font-semibold rounded-md border border-slate-200 text-slate-900" type="button" onClick={() => onDelete(file.id)}>
                                        Delete
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </li>
            ))}
        </ul>
    );
};

export default FileList;
