import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-view-lead',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './view-lead.component.html',
  styleUrls: ['./view-lead.component.css'],
})
export class ViewLeadComponent {
  leads: any[] = [];
  filteredLeads: any[] = [];
  searchQuery: string = '';
  sortBy: string = '';
  selectedLead: any = null;

  private apiUrl = 'https://localhost:7015/api/Lead';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchLeads();
  }

  getHeaders() {
    return {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      'Content-Type': 'application/json',
    };
  }

  fetchLeads() {
    this.http.get<any[]>(this.apiUrl, { headers: this.getHeaders() }).subscribe(
      (data) => {
        this.leads = data;
        this.filteredLeads = data;
      },
      (error) => {
        console.error('Error fetching leads:', error);
      }
    );
  }

  sortLeads() {
    if (this.sortBy === 'name') {
      this.filteredLeads.sort((a, b) => a.name.localeCompare(b.name));
    } else if (this.sortBy === 'status') {
      this.filteredLeads.sort((a, b) =>
        a.leadStatus.localeCompare(b.leadStatus)
      );
    } else if (this.sortBy === 'createdAt') {
      this.filteredLeads.sort(
        (a, b) =>
          new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime()
      );
    }
  }

  filterLeads() {
    this.filteredLeads = this.leads.filter((lead) =>
      lead.name.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  openEditModal(lead: any) {
    this.selectedLead = { ...lead };
  }

  closeEditModal() {
    this.selectedLead = null;
  }

  updateStatus() {
    if (!this.selectedLead) return;

    const url = `${this.apiUrl}/${this.selectedLead.leadId}/status`;
    this.http
      .patch(url, `"${this.selectedLead.leadStatus}"`, {
        headers: this.getHeaders(),
      })
      .subscribe(
        (response: any) => {
          alert(response.message);
          this.fetchLeads(); 
          this.closeEditModal();
        },
        (error) => {
          console.error('Error updating lead status:', error);
          alert('Failed to update status. Please try again.');
        }
      );
  }

  deleteLead(leadId: number) {
    if (confirm('Are you sure you want to delete this lead?')) {
      this.http
        .delete(`${this.apiUrl}/${leadId}`, { headers: this.getHeaders() })
        .subscribe(
          () => {
            this.leads = this.leads.filter((lead) => lead.leadId !== leadId);
            this.filteredLeads = this.filteredLeads.filter(
              (lead) => lead.leadId !== leadId
            );
            alert('Lead deleted successfully.');
          },
          (error) => {
            console.error('Error deleting lead:', error);
          }
        );
    }
  }


  convertToPipeline(leadId: number) {
    if (
      confirm('Are you sure you want to convert this lead to a sales pipeline?')
    ) {
      const url = `${this.apiUrl}/${leadId}/convert-to-pipeline`;

      this.http.post(url, null, { headers: this.getHeaders() }).subscribe(
        (response: any) => {
          alert(response.message);
          this.fetchLeads(); 
        },
        (error) => {
          console.error('Error converting lead to pipeline:', error);
          alert('Failed to convert lead to pipeline. Please try again.');
        }
      );
    }
  }
}
