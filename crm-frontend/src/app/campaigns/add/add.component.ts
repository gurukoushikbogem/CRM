import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RouterLink, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';

@Component({
  selector: 'app-add',
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    FormsModule,
    HttpClientModule,
  ],
  templateUrl: './add.component.html',
  styleUrl: './add.component.css',
})
export class AddComponent {
  private apiUrl = 'https://localhost:7015/api/Campaign';
  newCampaign = {
    name: '',
    startDate: '',
    endDate: '',
    targetSegment: '',
    budget: 0,
    status: '',
    Notes: '',
  };
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient) {}

  addCampaign() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http.post(this.apiUrl, this.newCampaign, { headers }).subscribe(
      (response: any) => {
        alert('Campaign created successfully.');
      },
      (error) => {
        console.error('Error adding campaign:', error);
        alert('Failed to create campaign. Please try again.');
      }
    );
  }
}
