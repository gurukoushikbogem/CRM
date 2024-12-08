import { Component } from '@angular/core';
import { Task } from '../task.model';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [HttpClientModule, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css'],
})
export class TasksComponent {
  tasks: Task[] = [];
  filteredTasks: Task[] = [];
  searchStatus: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks() {
    const headers = {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
    };

    this.http
      .get<Task[]>('https://localhost:7015/api/Task', { headers })
      .subscribe(
        (data) => {
          this.tasks = data;
          this.filteredTasks = data;
        },
        (error) => console.error('Error loading tasks:', error)
      );
  }

  markTaskAsDone(taskId: number) {
    const taskToUpdate = this.tasks.find((t) => t.taskId === taskId);
    if (taskToUpdate) {
      const updatedTask = {
        ...taskToUpdate,
        status: 'Completed',
        completedAt: new Date(),
      };
      const headers = {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        'Content-Type': 'application/json',
      };

      this.http
        .put('https://localhost:7015/api/Task', updatedTask, { headers })
        .subscribe(
          () => {
            this.loadTasks();
          },
          (error) => console.error('Error marking task as done:', error)
        );
    }
  }

  deleteTask(taskId: number) {
    if (confirm('Are you sure you want to delete this task?')) {
      const headers = {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      };

      this.http
        .delete(`https://localhost:7015/api/Task/${taskId}`, { headers })
        .subscribe(
          () => this.loadTasks(),
          (error) => console.error('Error deleting task:', error)
        );
    }
  }

  filterTasks() {
    const search = this.searchStatus.toLowerCase();
    this.filteredTasks = this.tasks.filter((task) =>
      task.status.toLowerCase().includes(search)
    );
  }

  isOverdue(dueDate: string): boolean {
    const today = new Date();
    return new Date(dueDate) < today;
  }

  contactLeadByEmail(taskDescription: string) {
    console.log('Task description:', taskDescription);
    if (taskDescription) {
      const leadName = taskDescription.split(': ')[1]?.trim();
      console.log('Lead name:', leadName);
      if (leadName) {
        const headers = {
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        };

        this.http
          .get<any>(`https://localhost:7015/api/Lead/name/${leadName}`, {
            headers,
          })
          .subscribe(
            (leadDetails) => {
              if (leadDetails.contactDetails) {
                const emailData = {
                  toEmail: leadDetails.contactDetails,
                  subject: 'Follow-up on Your Enquiry',
                  body: 'Hello, this is a follow-up regarding the lead you have shown interest in.',
                };

                this.http
                  .post('https://localhost:7015/api/Email/send', emailData, {
                    headers,
                    responseType: 'text',
                  })
                  .subscribe(
                    (response) => {
                      console.log('Response from API:', response); 
                      alert(response);
                      this.addCommunicationHistoryEntry(
                        leadDetails.id,
                        'Email',
                        'Follow-up on Your Enquiry',
                        true
                      );
                    },
                    (error) => console.error('Error sending email:', error)
                  );
              } else {
                alert('No contact details found for this lead.');
              }
            },
            (error) => console.error('Error fetching lead details:', error)
          );
      } else {
        alert('Lead name is not available in the task description.');
      }
    } else {
      alert('Task description is empty.');
    }
  }

  addCommunicationHistoryEntry(
    customerId: number,
    interactionType: string,
    notes: string,
    followUpRequired: boolean
  ) {
    console.log('Adding communication history entry...');
    const communicationHistoryEntry = {
      customerId: 1,
      interactionType: interactionType,
      date: new Date(),
      notes: notes,
      followUpRequired: followUpRequired,
      followUpStatus: followUpRequired ? 'Pending' : 'Completed',
      createdBy: 'Admin',
    };

    const headers = {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
    };

    this.http
      .post(
        'https://localhost:7015/api/CommunicationHistory',
        communicationHistoryEntry,
        { headers }
      )
      .subscribe(
        () => console.log('Communication history entry added successfully!'),
        (error) =>
          console.log('Error adding communication history entry:', error)
      );
  }
}
