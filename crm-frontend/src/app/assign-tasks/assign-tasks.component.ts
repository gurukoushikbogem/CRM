import { CommonModule } from '@angular/common';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-assign-tasks',
  standalone: true,
  imports: [HttpClientModule, CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './assign-tasks.component.html',
  styleUrls: ['./assign-tasks.component.css'],
})
export class AssignTasksComponent {
  unassignedLeads: any[] = [];
  salesRepresentatives: any[] = [];
  private baseUrl = 'https://localhost:7015/api';
  private authToken = localStorage.getItem('authToken') || ''; 
  
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadUnassignedLeads();
    this.loadSalesRepresentatives();
  }

  private getAuthHeaders(): HttpHeaders {
    return new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
  }

  loadUnassignedLeads(): void {
    const headers = this.getAuthHeaders();
    this.http
      .get<any[]>(`${this.baseUrl}/Lead/unassigned`, { headers })
      .subscribe(
        (data) => {
          this.unassignedLeads = data;
        },
        (error) => {
          console.error('Error fetching unassigned leads:', error);
        }
      );
  }

  loadSalesRepresentatives(): void {
    const headers = this.getAuthHeaders();
    const role = 'Sales Representative';
    this.http
      .get<any[]>(`${this.baseUrl}/User/role/${role}`, { headers })
      .subscribe(
        (data) => {
          this.salesRepresentatives = data.filter((user) => user.role === role);
        },
        (error) => {
          console.error('Error fetching sales representatives:', error);
        }
      );
  }

  assignLead(lead: any): void {
    if (!lead.assignedTo) {
      console.log('No salesperson selected for lead:', lead.name);
      alert('Please select a salesperson before assigning the lead.');
      return;
    }

    const taskPayload = {
      assignedTo: lead.assignedTo,
      taskDescription: `Follow up on lead: ${lead.name}`,
      dueDate: new Date(new Date().setDate(new Date().getDate() + 7)),
      status: 'Pending',
      priority: 'Medium',
      createdAt: new Date(),
    };

    const headers = this.getAuthHeaders();
    this.http.post(`${this.baseUrl}/Task`, taskPayload, { headers }).subscribe(
      (response: any) => {
        console.log('Task created successfully:', response);
        this.refreshUnassignedLeads(lead.leadId);
        alert('Task created successfully!');
      },
      (error) => {
        console.error('Error creating task:', error);
        alert('Failed to create task. Please try again.');
      }
    );
  }

  refreshUnassignedLeads(leadId: number): void {
    this.unassignedLeads = this.unassignedLeads.filter(
      (lead) => lead.leadId !== leadId
    );
  }
}
