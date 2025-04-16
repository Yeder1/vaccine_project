export interface VaccineTypeDTO {
  id: number;
  description?: string;
  vaccineTypeCode: string;
  vaccineTypeName: string; 
  isActive: boolean;
  isDeleted: boolean;
}
