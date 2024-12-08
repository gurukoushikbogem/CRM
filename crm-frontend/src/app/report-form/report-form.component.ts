import { Component } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-report-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './report-form.component.html',
  styleUrls: ['./report-form.component.css'],
})
export class ReportFormComponent {
  report = {
    type: '',
    startDate: '',
    endDate: '',
    format: 'PDF',
  };

  reportTypes = ['Sales Performance'];
  reportFormats = ['PDF'];

  constructor(private http: HttpClient) {}

  onSubmit(): void {
    if (!this.report.startDate || !this.report.endDate) {
      alert('Start Date and End Date are required!');
      return;
    }

    const apiUrl = 'https://localhost:7015/api/User/sales-report';

    const params = new HttpParams()
      .set('startDate', this.report.startDate)
      .set('endDate', this.report.endDate);

    const headers = new HttpHeaders({
      Authorization: `Bearer ${localStorage.getItem('authToken')}`, 
      'Content-Type': 'application/json', 
      'Custom-Header': 'CustomValue', 
    });

    this.http.get(apiUrl, { params, headers, responseType: 'blob' }).subscribe(
      (response) => {
        const blob = new Blob([response], {
          type: this.getMimeType(this.report.format),
        });
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `Sales_Report_${this.formatDate(
          this.report.startDate
        )}_to_${this.formatDate(
          this.report.endDate
        )}.${this.report.format.toLowerCase()}`;
        a.click();
        window.URL.revokeObjectURL(url);
        alert('Report generated successfully!');
      },
      (error) => {
        console.error('Error generating report:', error);
        alert('Failed to generate the report. Please try again.');
      }
    );
  }

  private formatDate(date: string): string {
    return new Date(date).toISOString().split('T')[0];
  }

  private getMimeType(format: string): string {
    switch (format.toLowerCase()) {
      case 'pdf':
        return 'application/pdf';
      case 'excel':
        return 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet';
      case 'json':
        return 'application/json';
      default:
        return 'application/octet-stream';
    }
  }
}
