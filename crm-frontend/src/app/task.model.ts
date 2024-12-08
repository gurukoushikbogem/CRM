export interface Task {
  taskId?: number;
  customerId: number;
  taskDescription: string;
  dueDate: string;
  status: string;
  priority: string;
  createdAt: Date;
  completedAt: Date | null;
}
