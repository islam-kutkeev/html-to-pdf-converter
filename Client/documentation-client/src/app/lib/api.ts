import axios from 'axios';
import { FileInformation } from './definitions';

export const API_BASE_URL = 'http://localhost:5271';


export const fetchFiles = async (): Promise<FileInformation[]> => {
    try {
        const response = await axios.get(`${API_BASE_URL}/get-all`);
        if (response.data.code === 0) {
            return response.data.data;
        }
        console.error(`Error from service: ${response.data.message}`);
    } catch (error) {
        console.error('Error deleting file:', error);
    }

    return [];
};

export const uploadFile = async (selectedFile: File | null) => {
    if (!selectedFile) {
        console.error('No file selected');
        return;
    }

    const formData = new FormData();
    formData.append('file', selectedFile);

    try {
        const response = await axios.post(`${API_BASE_URL}/save`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });
    } catch (error) {
        console.error('Error uploading file:', error);
    }
};

export const downloadFile = async (file: FileInformation) => {
    try {
        const response = await axios.get(`${API_BASE_URL}/download/${file.id}`, {
            responseType: 'arraybuffer',
        });

        const blob = new Blob([response.data], { type: response.headers['content-type'] });

        const link = document.createElement('a');
        link.href = window.URL.createObjectURL(blob);
        link.download = `${file.name}.pdf`;
        link.click();
    } catch (error) {
        console.error('Error downloading file:', error);
    }
};

export const deleteFile = async (id: string) => {
    try {
        await axios.delete(`${API_BASE_URL}/delete/${id}`);
    } catch (error) {
        console.error('Error deleting file:', error);
    }
};
