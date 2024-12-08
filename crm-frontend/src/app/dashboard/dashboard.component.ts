import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterLink, RouterModule } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterModule, HttpClientModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css',
})
export class DashboardComponent {
  userRole: string = '';
  submenu: { [key: string]: boolean } = {};

  personName: string | null = null;

  isProfileDropdownOpen = false;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const userDetails = localStorage.getItem('userDetails');
    if (userDetails) {
      const parsedDetails = JSON.parse(userDetails);
      this.personName = parsedDetails.username;
      this.userRole = parsedDetails.role;
    } else {
      this.personName = 'Guest';
    }
  }

  logout(): void {
    localStorage.removeItem('userDetails');
    localStorage.removeItem('token');
    this.router.navigate(['']);
  }

  toggleSubmenu(submenuName: string) {
    this.submenu[submenuName] = !this.submenu[submenuName];
  }

  checkRole(role: string): boolean {
    return this.userRole === role || this.userRole === 'admin';
  }

  toggleProfileDropdown(): void {
    this.isProfileDropdownOpen = !this.isProfileDropdownOpen;
  }

  isSidebarOpen: boolean = true; 

  toggleSidebar(): void {
    this.isSidebarOpen = !this.isSidebarOpen;
  }
}
