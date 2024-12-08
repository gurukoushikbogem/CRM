import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-support',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css'],
})
export class SupportComponent {
  ticket = {
    customerId: null,
    issueDescription: '',
    priority: 'Normal',
    notes: '',
  };

  apiUrl = 'https://localhost:7015/api/SupportTicket';
  loading: boolean = false;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    const userDetails = localStorage.getItem('userDetails');
    if (userDetails) {
      const parsedDetails = JSON.parse(userDetails);
      this.ticket.customerId = parsedDetails.id;
    }
  }

  onSubmit() {
    if (!this.ticket.customerId) {
      alert('Customer ID is required. Please log in first.');
      return;
    }

    if (!this.ticket.issueDescription) {
      alert('Issue Description is required.');
      return;
    }

    this.loading = true;

    // Add Authorization header
    const headers = {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      'Content-Type': 'application/json',
    };

    this.http.post(this.apiUrl, this.ticket, { headers }).subscribe({
      next: (response: any) => {
        alert('Support ticket submitted successfully!');
        console.log('Response:', response);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        alert('Failed to submit the support ticket. Please try again.');
      },
      complete: () => {
        this.loading = false;
      },
    });
  }

  resetForm() {
    this.ticket.issueDescription = '';
    this.ticket.priority = 'Normal';
    this.ticket.notes = '';
    this.ticket.customerId = null;
  }
}
