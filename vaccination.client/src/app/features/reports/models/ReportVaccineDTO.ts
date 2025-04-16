export interface ReportVaccineDTO {
    no: number;
    vaccineName: string;
    vaccineType: string;
    beginNextInjectDate: Date;
    endNextInjectDate: Date;
    origin: string;
    numOfInject: number;
  }