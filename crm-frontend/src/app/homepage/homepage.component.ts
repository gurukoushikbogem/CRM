import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

interface LoginResponse {
  user: {
    id: number;
    username: string;
    email: string;
    role: string;
    lastLoginAt: string;
  };
  token: string;
}

@Component({
  selector: 'app-homepage',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule, RouterModule],
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css'],
})

export class HomepageComponent {
  activeTab: 'login' | 'signup' = 'login';

  loginForm: FormGroup;
  signupForm: FormGroup;

  private apiUrl = 'https://localhost:7015/api/User';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.signupForm = this.fb.group({
      username: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  setActiveTab(tab: 'login' | 'signup') {
    this.activeTab = tab;
  }

  login() {
    if (this.loginForm.valid) {
      this.http
        .post<LoginResponse>(`${this.apiUrl}/login`, this.loginForm.value)
        .subscribe({
          next: (response) => {
            console.log('Login Successful:', response);
            alert('Login Successful');

            localStorage.setItem('userDetails', JSON.stringify(response.user));
            localStorage.setItem('authToken', response.token);

            this.router.navigate(['/dashboard/campaign/list']);
          },
          error: (err) => {
            console.error('Login Failed:', err);
            alert('Invalid credentials, please try again.');
          },
        });
    } else {
      alert('Please fill in all required fields.');
    }
  }

  register() {
    if (this.signupForm.valid) {
      this.http
        .post(`${this.apiUrl}/register`, this.signupForm.value)
        .subscribe({
          next: (response) => {
            console.log('Signup Successful:', response);
            alert('Signup Successful, You can Login now');
            this.setActiveTab('login'); 
          },
          error: (err) => {
            console.error('Signup Failed:', err);
            alert('Signup failed, please try again.');
          },
        });
    } else {
      alert('Please fill in all required fields.');
    }
  }
}
