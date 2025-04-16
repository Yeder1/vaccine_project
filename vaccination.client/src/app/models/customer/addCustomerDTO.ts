export interface AddCustomerDTO {
    id: number;                  
    fullName: string;            
    dateOfBirth: string|null;        
    gender: boolean;             
    identityCard: string;       
    address: string | null;  
    username: string;            
    password: string;            
    confirmPassword: string;    
    email: string;              
    phone: string;               
  }