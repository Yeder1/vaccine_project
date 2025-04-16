export interface InjectionResultVaccineRequest {
    id?: number;
    customerId: number;
    injectionDate: Date;
    injectionPlace: string;
    nextInjectionDate?: Date;
    numberOfInjection: number;
    prevention: string;
    vaccineId: number;
}
