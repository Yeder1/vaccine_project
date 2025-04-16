import { Employee } from "./employee";

export interface AddEmployeeRequest {
  Employee: Employee;
  Image?: File;
}