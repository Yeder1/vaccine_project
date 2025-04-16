import { VaccineTypeDTO } from "../../../models/vaccineType/vacineTypeDTO";

export interface VaccineDTO {
  id?: number;
  contraindication?: string;
  indication?: string; 
  numberOfInjection?: number; 
  origin?: string; 
  nextBeginNextInjection?: Date; 
  nextEndNextInjection?: Date; 
  usage?: string;
  vaccineName?: string; 
  vaccineTypeId: number; 
  vaccineTypeDTO?: VaccineTypeDTO; 
  status?: boolean
}
