import { CommonModule } from '@angular/common';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-opputunities',
  standalone: true,
  imports: [FormsModule, HttpClientModule, CommonModule],
  templateUrl: './opputunities.component.html',
  styleUrl: './opputunities.component.css',
})
export class OpputunitiesComponent {
  opportunities: any[] = [];
  filteredOpportunities: any[] = [];
  searchQuery: string = '';
  filterByHealth: string = '';

  private apiUrl = 'https://localhost:7015/api/Opportunity';
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchOpportunities();
  }

  fetchOpportunities() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });

    this.http.get<any[]>(this.apiUrl, { headers }).subscribe(
      (data) => {
        this.opportunities = data;
        this.filteredOpportunities = data;
      },
      (error) => {
        console.error('Error fetching opportunities:', error);
      }
    );
  }

  filterOpportunities() {
    this.filteredOpportunities = this.opportunities.filter((opportunity) =>
      this.filterByHealth
        ? opportunity.accountHealth.toLowerCase() ===
          this.filterByHealth.toLowerCase()
        : true
    );

    if (this.searchQuery) {
      this.filteredOpportunities = this.filteredOpportunities.filter(
        (opportunity) =>
          opportunity.title
            .toLowerCase()
            .includes(this.searchQuery.toLowerCase())
      );
    }
  }

  deleteOpportunity(opportunityId: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });

    if (confirm('Are you sure you want to delete this opportunity?')) {
      this.http
        .delete(`${this.apiUrl}/${opportunityId}`, { headers })
        .subscribe(
          () => {
            this.opportunities = this.opportunities.filter(
              (opportunity) => opportunity.opportunityId !== opportunityId
            );
            this.filteredOpportunities = this.filteredOpportunities.filter(
              (opportunity) => opportunity.opportunityId !== opportunityId
            );
            alert('Opportunity deleted successfully.');
          },
          (error) => {
            console.error('Error deleting opportunity:', error);
          }
        );
    }
  }

  convertToCustomer(opportunityId: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });

    if (
      confirm(
        'Are you sure you want to convert this opportunity to a customer?'
      )
    ) {
      this.http
        .post(`${this.apiUrl}/${opportunityId}/convert`, {}, { headers })
        .subscribe(
          (response: any) => {
            alert('Opportunity converted to customer successfully!');
            console.log('Converted Customer Details:', response);
            this.fetchOpportunities();
          },
          (error) => {
            console.error('Error converting opportunity to customer:', error);
          }
        );
    }
  }
}
