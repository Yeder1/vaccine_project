import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { InjectionResultCustomerDTO, VaccinationResultDTO } from 'src/app/models/vaccinationResult/vaccinationResultDTO';
import { API_URL } from 'src/app/core/contants/url';
import { InjectionResultCustomersDTOResponse, VaccinationResultDTOResponse, VaccinationResultListResponse, VaccineTypeDTOsResponse } from 'src/app/models/vaccinationResult/vaccinationResultResponse';
import { VaccineTypeDTO } from 'src/app/models/vaccinationResult/vaccineTypeDTO';
import { InjectionResultVaccineRequest } from 'src/app/models/vaccinationResult/vaccinationResultRequest';
@Injectable({
    providedIn: 'root'
})

export class VaccinationResultService {

    private endpoint = API_URL + '/InjectionResult';
    constructor(private client: HttpClient) { }

    getAllVaccinationResults(): Observable<VaccinationResultListResponse> {
        return this.client.get<VaccinationResultListResponse>(this.endpoint);
    }

    searchByKeyWord(keyword: string): Observable<VaccinationResultListResponse> {
        return this.client.get<VaccinationResultListResponse>(`${this.endpoint}/search?keyword=${keyword}`);
    }
    getVaccinationResultById(id: string): Observable<VaccinationResultDTOResponse> {
        return this.client.get<VaccinationResultDTOResponse>(this.endpoint + `/${id}`);
    }

    deleteVaccinationResult(vaccinationResult: VaccinationResultDTO): Observable<void> {
        return this.client.delete<void>(this.endpoint, { body: vaccinationResult });
    }

    deleteManyVaccinationResult(vaccinationResult: VaccinationResultDTO[]): Observable<void> {
        return this.client.delete<void>(this.endpoint + `/deleteMany`, { body: vaccinationResult });
    }

    deleteVaccinationResultById(id: string): Observable<void> {
        return this.client.delete<void>(this.endpoint + `/${id}`);
    }

    createVaccinationResult(vaccinationResult: InjectionResultVaccineRequest): Observable<void> {
        return this.client.post<void>(this.endpoint, vaccinationResult);
    }

    updateVaccinationResult(vaccinationResult: InjectionResultVaccineRequest): Observable<void> {
        return this.client.put<void>(this.endpoint, vaccinationResult);
    }

    getAllVaccineType(): Observable<VaccineTypeDTOsResponse> {
        return this.client.get<VaccineTypeDTOsResponse>(`${this.endpoint}/get-all-vaccine-type`);
    }

    getAllCustomer(): Observable<InjectionResultCustomersDTOResponse> {
        return this.client.get<InjectionResultCustomersDTOResponse>(`${this.endpoint}/get-all-vaccine-customer`);
    }
}
