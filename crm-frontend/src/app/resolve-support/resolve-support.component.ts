import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { catchError, of } from 'rxjs';

interface SupportTicket {
  ticketId: number;
  customerId: number;
  issueDescription: string;
  assignedTo?: number;
  ticketStatus: string;
  createdAt: string;
  updatedAt?: string;
  resolutionDate?: string;
  slaDeadline?: string;
  priority: string;
  notes?: string;
  status: string;
}

@Component({
  selector: 'app-resolve-support',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './resolve-support.component.html',
  styleUrl: './resolve-support.component.css',
})
export class ResolveSupportComponent implements OnInit {
  tickets: SupportTicket[] = [];
  loading: boolean = false;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchTickets();
  }

  fetchTickets(): void {
    this.loading = true;
    this.http
      .get<SupportTicket[]>(
        'https://localhost:7015/api/SupportTicket/status/Open',
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`,
          },
        }
      )
      .subscribe(
        (data) => {
          this.tickets = data;
          this.loading = false;
        },
        (error) => {
          console.error('Error fetching tickets:', error);
          this.loading = false;
        }
      );
  }

  markAsResolved(ticketId: number) {
    this.loading = true;

    this.http
      .put(
        `https://localhost:7015/api/SupportTicket/status/${ticketId}`,
        '"Resolved"',
        {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('authToken')}`,
          },
        }
      )
      .pipe(
        catchError((error) => {
          console.error('Error updating ticket status', error);
          alert('Failed to update ticket status.');
          this.loading = false;
          return of(null);
        })
      )
      .subscribe((response: any) => {
        if (response?.message) {
          alert(response.message);
          const ticket = this.tickets.find((t) => t.ticketId === ticketId);
          if (ticket) {
            ticket.ticketStatus = 'Resolved';
          }
        }
        this.loading = false;
      });
  }
}
