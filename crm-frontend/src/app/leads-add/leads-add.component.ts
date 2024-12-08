import { CommonModule } from '@angular/common';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-leads-add',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, CommonModule, HttpClientModule],
  templateUrl: './leads-add.component.html',
  styleUrl: './leads-add.component.css',
})
export class LeadsAddComponent {
  leadData = {
    leadSource: '',
    name: '',
    contactDetails: '',
    leadStatus: 'New',
    potentialValue: 0,
    salesStage: '',
    createdAt: new Date().toISOString(),
    updatedAt: new Date().toISOString(),
  };

  private apiUrl = 'https://localhost:7015/api/Lead';
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient) {}

  addLead() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });

    this.http
      .post<{ message: string }>(this.apiUrl, this.leadData, { headers })
      .subscribe(
        (response) => {
          alert(response.message);
          this.resetForm();
        },
        (error) => {
          console.error('Error adding lead:', error);
          alert('Failed to add lead. Please try again.');
        }
      );
  }

  resetForm() {
    this.leadData = {
      leadSource: '',
      name: '',
      contactDetails: '',
      leadStatus: 'New',
      potentialValue: 0,
      salesStage: '',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString(),
    };
  }
}
