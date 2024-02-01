export type FileInformation = {
    id: string;
    createdAt: string;
    name: string;
    status: 'New' | 'InProgress' | 'Converted' | 'Error';
};

export type ServiceResponse<T> = {
    code: number;
    message: string;
    data: T;
};