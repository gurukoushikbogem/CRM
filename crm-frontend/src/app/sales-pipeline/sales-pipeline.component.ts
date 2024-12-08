import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-sales-pipeline',
  standalone: true,
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './sales-pipeline.component.html',
  styleUrl: './sales-pipeline.component.css',
})
export class SalesPipelineComponent {
  salesPipeline: any[] = [];
  filteredPipeline: any[] = [];
  searchQuery: string = '';
  filterByStage: string = '';
  pipelineStages: string[] = [
    'Prospecting',
    'Open',
    'Proposal Sent',
    'Closed Lost',
    'Qualified',
  ];

  private apiUrl = 'https://localhost:7015/api/SalesPipeline';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchSalesPipeline();
  }

  fetchSalesPipeline() {
    this.http
      .get<any[]>(this.apiUrl, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        },
      })
      .subscribe(
        (data) => {
          this.salesPipeline = data;
          this.filteredPipeline = data;
        },
        (error) => {
          console.error('Error fetching sales pipeline:', error);
        }
      );
  }

  filterPipeline() {
    this.filteredPipeline = this.salesPipeline.filter((pipeline) => {
      const matchesStage = this.filterByStage
        ? pipeline.stage.toLowerCase() === this.filterByStage.toLowerCase()
        : true;

      const matchesSearch = this.searchQuery
        ? pipeline.name.toLowerCase().includes(this.searchQuery.toLowerCase())
        : true;

      return matchesStage && matchesSearch;
    });
  }

  deletePipeline(pipelineId: number) {
    if (confirm('Are you sure you want to delete this pipeline?')) {
      this.http
        .delete(`${this.apiUrl}/${pipelineId}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('authToken')}`,
          },
        })
        .subscribe(
          () => {
            this.salesPipeline = this.salesPipeline.filter(
              (pipeline) => pipeline.pipelineId !== pipelineId
            );
            this.filteredPipeline = this.filteredPipeline.filter(
              (pipeline) => pipeline.pipelineId !== pipelineId
            );
            alert('Pipeline deleted successfully.');
          },
          (error) => {
            console.error('Error deleting pipeline:', error);
          }
        );
    }
  }

  createOpportunity(pipelineId: number) {
    if (
      confirm(
        'Are you sure you want to create an opportunity for this pipeline?'
      )
    ) {
      const url = `${this.apiUrl}/pipeline/${pipelineId}/create-opportunity`;
      this.http
        .post<any>(
          url,
          {},
          {
            headers: {
              Authorization: `Bearer ${localStorage.getItem('authToken')}`,
            },
          }
        )
        .subscribe(
          (response) => {
            alert(response.message || 'Opportunity created successfully.');
            console.log('Created Opportunity:', response.opportunity);
          },
          (error) => {
            console.error('Error creating opportunity:', error);
            alert('Failed to create opportunity.');
          }
        );
    }
  }

  selectedPipeline: any = null;
  newStage: string = '';

  openChangeStageModal(pipeline: any) {
    this.selectedPipeline = pipeline;
    this.newStage = pipeline.stage;
  }

  updatePipelineStage() {
    if (!this.newStage || !this.selectedPipeline) return;

    const url = `${this.apiUrl}/${this.selectedPipeline.pipelineId}/stage`;
    this.http
      .patch<any>(url, JSON.stringify(this.newStage), {
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        },
      })
      .subscribe(
        (response) => {
          alert(response.message || 'Pipeline stage updated successfully.');
          this.selectedPipeline.stage = this.newStage;
          this.closeChangeStageModal();
        },
        (error) => {
          console.error('Error updating pipeline stage:', error);
          alert('Failed to update pipeline stage.');
        }
      );
  }

  closeChangeStageModal() {
    this.selectedPipeline = null;
    this.newStage = '';
  }
}
