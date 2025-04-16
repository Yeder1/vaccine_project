import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { Router } from '@angular/router';
import { ExampleFlatNode, MenuNode } from 'src/app/models/menu';
import { User } from 'src/app/models/user';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  user: User = {
    avatar: './../../../../../assets/avatar.jpg',
    name: 'Admin',
    email: 'admin@gmail.com',
  };
  menu: MenuNode[] = [
    {
      name: 'Home',
      routerLink: '/home',
    },
    {
      name: 'News',
      routerLink: '/news',
      children: [
        {
          name: 'News List',
          routerLink: '/news/list',
        },
        {
          name: 'Create News',
          routerLink: '/news/add',
        },
      ],
    },
    {
      name: 'Vaccination Result',
      routerLink: '/vaccination-result-management',
      children: [
        {
          name: 'Vaccination Result List',
          routerLink: '/vaccination-result-management/list',
        },
        {
          name: 'Add Vaccination Result',
          routerLink: '/vaccination-result-management/add',
        },
      ],
    },{
      name: 'Customer',
      children: [
        {
          name: 'Customer List',
          routerLink: '/customer',
        },
        {
          name: 'Customer Create',
          routerLink: '/customer/add',
        },
      ],
    },
    {
      name: 'Employee',
      children: [
        {
          name: 'Employee List',
          routerLink: '/employee/list',
        },
        {
          name: 'Add Employee',
          routerLink: '/employee/add',
        }
      ],
    },
    {
      name: 'Injection Schedule',
      routerLink: '/injection-schedule',
      children: [
        {
          name: 'Injection Schedule List',
          routerLink: '/injection-schedule/list',
        },
        {
          name: 'Create Injection Schedule ',
          routerLink: '/injection-schedule/add',
        },
      ],
    },
    {
      name: 'Vaccine Type',
      children: [
        {
          name: 'Vaccine Type List',
          routerLink: '/vaccine-type',
        },
        {
          name: 'Create Vaccine Type',
          routerLink: '/vaccine-type/add',
        },
      ],
    },

    {
      name: 'Report',
      routerLink: '/reports',
      children: [
        {
          name: 'Report Injection',
          routerLink: '/reports/report-injection-result',
        },
        {
          name: 'Report Customer',
          routerLink: '/reports/report-customer-result',
        },
        {
          name: 'Report Vaccine',
          routerLink: '/reports/report-vaccine-result',
        },
      ],
    },
  ];
  constructor(private router: Router) {
    this.dataSource.data = this.menu;
  }

  ngOnInit() { }

  logout() {
    localStorage.clear();
    this.router.navigate(['/auth/login']);
  }

  performAction(node: { name: string }) {
    const routerLink = this.findRouterLinkByName(node.name, this.menu);
    if (routerLink) {
      this.router.navigate([routerLink]);
    }
  }

  findRouterLinkByName(name: string, nodes: MenuNode[]): string | undefined {
    for (const item of nodes) {
      if (item.name === name) {
        return item.routerLink;
      }
      if (item.children) {
        const childRouterLink = this.findRouterLinkByName(name, item.children);
        if (childRouterLink) {
          return childRouterLink;
        }
      }
    }
    return undefined; // Return undefined if no match is found
  }

  private _transformer = (node: MenuNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    (node) => node.level,
    (node) => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.children
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;
}
