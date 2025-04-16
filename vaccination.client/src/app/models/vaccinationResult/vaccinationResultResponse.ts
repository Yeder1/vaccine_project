import { InjectionResultCustomerDTO, VaccinationResultDTO } from "./vaccinationResultDTO";
import { VaccineTypeDTO } from "./vaccineTypeDTO";

export interface VaccinationResultListResponse {
    message: string;
    data: VaccinationResultDTO[];
}

export interface VaccineTypeDTOsResponse {
    message: string;
    data: VaccineTypeDTO[];
}
export interface VaccinationResultDTOResponse {
    message: string;
    data: VaccinationResultDTO;
}

export interface InjectionResultCustomersDTOResponse {
    message: string;
    data: InjectionResultCustomerDTO[];
}
