import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {
  HttpClient,
  HttpClientModule,
  HttpHeaders,
} from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css',
})
export class ListComponent {
  campaigns: any[] = [];
  private apiUrl = 'https://localhost:7015/api/Campaign';
  private authToken = localStorage.getItem('authToken') || '';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.fetchCampaigns();
  }

  fetchCampaigns() {
    const headers = new HttpHeaders({
      Authorization: `Bearer ${this.authToken}`,
    });
    this.http.get<any[]>(this.apiUrl, { headers }).subscribe(
      (data) => {
        this.campaigns = data;
      },
      (error) => {
        console.error('Error fetching campaigns:', error);
      }
    );
  }

  navigateToLeadForm(campaignId: number) {
    this.router.navigate(['dashboard/add-leads']);
  }
}
