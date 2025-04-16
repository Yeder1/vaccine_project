export interface ErrorDetail {
    [key: string]: string;
}

export interface ErrorResponseValidationAttributes {
    error: {
        errors: ErrorDetail;
    };
}
