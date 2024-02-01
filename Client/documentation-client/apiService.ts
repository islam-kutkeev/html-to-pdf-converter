// apiService.ts
import axios, { AxiosResponse } from 'axios';
import { File } from './types/file';

interface ApiResponse<T> {
  data: T;
  code: number;
  message: string;
}

const API_BASE_URL = 'http://localhost:5271';

const handleApiResponse = <T>(response: AxiosResponse<ApiResponse<T>>): T => {
  const { data, code, message } = response.data;

  if (code === 0) {
    return data;
  } else {
    throw new Error(`API Error: ${message}`);
  }
};

export const getFiles = async (): Promise<File[]> => {
  try {
    const response = await axios.get<ApiResponse<File[]>>(
      `${API_BASE_URL}/get-all`
    );
    return handleApiResponse(response);
  } catch (error) {
    console.error('Error fetching files:', error);
    throw error;
  }
};

export const addFile = async (newFile: { name: string }): Promise<void> => {
  try {
    const response = await axios.post<ApiResponse<void>>(
      `${API_BASE_URL}/files`,
      newFile
    );
    handleApiResponse(response);
  } catch (error) {
    console.error('Error adding file:', error);
    throw error;
  }
};

export const deleteFile = async (fileId: string): Promise<void> => {
  try {
    const response = await axios.delete<ApiResponse<void>>(
      `${API_BASE_URL}/files/${fileId}`
    );
    handleApiResponse(response);
  } catch (error) {
    console.error('Error deleting file:', error);
    throw error;
  }
};
