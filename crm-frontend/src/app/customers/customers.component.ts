import { CommonModule } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './customers.component.html',
  styleUrl: './customers.component.css',
})
export class CustomersComponent {
  customers: any[] = [];
  selectedCustomer: any = null;
  showAddCustomerForm = false;
  searchQuery = '';
  newCustomer = {
    name: '',
    company: '',
    industry: '',
    contactDetails: '',
  };
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient) {
    this.loadCustomers();
  }

  loadCustomers(): void {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get('https://localhost:7015/api/Customer', { headers })
      .subscribe((response: any) => {
        this.customers = response;
      });
  }

  viewCustomer(id: number): void {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get(`https://localhost:7015/api/Customer/${id}`, { headers })
      .subscribe((response: any) => {
        this.selectedCustomer = response;
      });
  }

  deleteCustomer(id: number): void {
    if (confirm('Are you sure you want to delete this customer?')) {
      const headers = new HttpHeaders({
        Authorization: `Bearer ${this.authToken}`,
      });
      this.http
        .delete(`https://localhost:7015/api/Customer/${id}`, { headers })
        .subscribe(() => {
          alert('Customer deleted successfully!');
          this.loadCustomers();
        });
    }
  }

  addCustomer(): void {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .post('https://localhost:7015/api/Customer', this.newCustomer, {
        headers,
      })
      .subscribe(() => {
        alert('Customer added successfully!');
        this.showAddCustomerForm = false;
        this.loadCustomers();
      });
  }

  searchCustomer(): void {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get(
        `https://localhost:7015/api/Customer/search?query=${this.searchQuery}`,
        { headers }
      )
      .subscribe((response: any) => {
        this.customers = response;
      });
  }

  closeCustomerDetails(): void {
    this.selectedCustomer = null;
  }

  toggleAddCustomerForm(): void {
    this.showAddCustomerForm = !this.showAddCustomerForm;
  }
}
