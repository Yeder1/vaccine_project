export interface VaccinationResultDTO {
    id?: number;
    prevention: string;
    vaccine: InjectionResultVaccineDTO;
    numberOfInjection?: number;
    injectionDate: Date;
    nextInjectionDate?: Date;
    injectionPlace: string;
    customer: InjectionResultCustomerDTO;
    isDeleted?: boolean;
}

export interface InjectionResultCustomerDTO {
    id?: number;
    fullName: string;
    phone?: string;
    identityCard?: string;
    age?: number;
    year?: number;
}

export interface InjectionResultVaccineDTO {
    id?: number;
    vaccineTypeName: string;
}
