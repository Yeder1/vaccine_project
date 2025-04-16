export enum InjectionScheduleStatus {
  NotYet = 'NotYet',
  Open = 'Open',
  Over = 'Over'
}

export interface InjectionSchedule {
  id?: number; // Assuming you have an ID field
  description?: string;
  startDate?: Date;
  endDate?: Date;
  place?: string;
  status: InjectionScheduleStatus;
  vaccineId: number;
  vaccineName?: string; // Add this line
}
