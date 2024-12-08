import { CommonModule } from '@angular/common';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-interaction',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './interaction.component.html',
  styleUrl: './interaction.component.css',
})
export class InteractionComponent {
  communicationHistory: any[] = [];
  newInteraction = {
    customerId: 1,
    interactionType: '',
    date: new Date().toISOString(),
    notes: '',
    followUpRequired: false,
    followUpDate: null,
    followUpStatus: 'Pending',
    createdBy: 'Salesperson',
  };
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchCommunicationHistory();
  }

  fetchCommunicationHistory() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get<any[]>('https://localhost:7015/api/CommunicationHistory', {
        headers,
      })
      .pipe(
        catchError((err) => {
          console.error('Error fetching data', err);
          return of([]);
        })
      )
      .subscribe((data) => {
        this.communicationHistory = data;
      });
  }

  addInteraction() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .post(
        'https://localhost:7015/api/CommunicationHistory',
        this.newInteraction,
        { headers }
      )
      .pipe(
        catchError((err) => {
          console.error('Error adding interaction', err);
          return of({ message: 'Error adding interaction' });
        })
      )
      .subscribe((response) => {
        console.log(response);
        this.fetchCommunicationHistory();
      });
  }

  editInteraction(id: number) {
    const updatedInteraction = {
      ...this.communicationHistory.find((item) => item.id === id),
      notes: 'Updated notes',
    };
    updatedInteraction.InteractionId = 1;
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .put(
        `https://localhost:7015/api/CommunicationHistory/${id}`,
        updatedInteraction,
        { headers }
      )
      .pipe(
        catchError((err) => {
          console.error('Error updating interaction', err);
          return of({ message: 'Error updating interaction' });
        })
      )
      .subscribe((response) => {
        console.log(response);
        this.fetchCommunicationHistory();
      });
  }

  deleteInteraction(id: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .delete(`https://localhost:7015/api/CommunicationHistory/${id}`, {
        headers,
      })
      .pipe(
        catchError((err) => {
          console.error('Error deleting interaction', err);
          return of({ message: 'Error deleting interaction' });
        })
      )
      .subscribe((response) => {
        console.log(response);
        this.fetchCommunicationHistory();
      });
  }

  getFollowUps() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get('https://localhost:7015/api/CommunicationHistory/follow-ups', {
        headers,
      })
      .pipe(
        catchError((err) => {
          console.error('Error fetching follow-ups', err);
          return of([]);
        })
      )
      .subscribe((data) => {
        console.log('Follow-ups:', data);
      });
  }

  getCommunicationHistoryByCustomer(customerId: number) {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http
      .get(
        `https://localhost:7015/api/CommunicationHistory/customer/${customerId}`,
        { headers }
      )
      .pipe(
        catchError((err) => {
          console.error('Error fetching customer history', err);
          return of([]);
        })
      )
      .subscribe((data) => {
        console.log('Customer-specific communication history:', data);
      });
  }
}
