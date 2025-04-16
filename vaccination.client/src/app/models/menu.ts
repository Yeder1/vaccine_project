export interface MenuNode {
  name: string;
  children?: MenuNode[];
  routerLink?: string;
}

export interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  routerLink?: string;
  icon?: string;
}
