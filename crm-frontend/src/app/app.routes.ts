import { Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LeadsAddComponent } from './leads-add/leads-add.component';
import { ViewLeadComponent } from './view-lead/view-lead.component';
import { SalesPipelineComponent } from './sales-pipeline/sales-pipeline.component';
import { OpputunitiesComponent } from './opputunities/opputunities.component';
import { AddComponent } from './campaigns/add/add.component';
import { ListComponent } from './campaigns/list/list.component';
import { SupportComponent } from './support/support.component';
import { ReportFormComponent } from './report-form/report-form.component';
import { InteractionComponent } from './interaction/interaction.component';
import { TasksComponent } from './tasks/tasks.component';
import { AssignTasksComponent } from './assign-tasks/assign-tasks.component';
import { CampaignLeadComponent } from './campaign-lead/campaign-lead.component';
import { CustomersComponent } from './customers/customers.component';
import { ResolveSupportComponent } from './resolve-support/resolve-support.component';

export const routes: Routes = [
  { path: '', component: HomepageComponent },
  {
    path: 'dashboard',
    component: DashboardComponent,
    children: [
      { path: 'leads/add', component: LeadsAddComponent },
      { path: 'leads/view', component: ViewLeadComponent },
      { path: 'sales-pipeline', component: SalesPipelineComponent },
      { path: 'oppurtunities', component: OpputunitiesComponent },
      { path: 'campaign/list', component: ListComponent },
      { path: 'campaign/add', component: AddComponent },
      { path: 'add-lead', component: LeadsAddComponent },
      { path: 'add-leads', component: CampaignLeadComponent },
      { path: 'support', component: SupportComponent },
      { path: 'reports/add', component: ReportFormComponent },
      { path: 'interaction', component: InteractionComponent },
      { path: 'tasks', component: TasksComponent },
      { path: 'assign/tasks', component: AssignTasksComponent },
      { path: 'customer', component: CustomersComponent },
      { path: 'resolve-support', component: ResolveSupportComponent },
    ],
  },
];
