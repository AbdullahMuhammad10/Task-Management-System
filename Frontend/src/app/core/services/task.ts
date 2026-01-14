import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TaskItem } from '../models/task';
import { Observable } from 'rxjs';
import { UpdateTaskDto } from '../models/update-task-dto';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  // Here The Base Url OF Our Api
  private apiUrl = 'https://localhost:7099/api/tasks';

  // Injecting HttpClient To Be Able To Make Http Requests
  constructor(private Http: HttpClient) {}

  // Method To Get All Tasks
  getAllTasks(): Observable<TaskItem[]> {
    return this.Http.get<TaskItem[]>(this.apiUrl);
  }

  // Method To Get A Task By Id
  getTaskById(id: number): Observable<TaskItem> {
    return this.Http.get<TaskItem>(`${this.apiUrl}/${id}`);
  }

  // Method To Create A New Task
  CreateTask(Task: Partial<TaskItem>): Observable<TaskItem> {
    return this.Http.post<TaskItem>(this.apiUrl, Task);
  }

  // Method To Update Task Information
  UpdateTask(Id: number, Task: UpdateTaskDto): Observable<void> {
    return this.Http.put<void>(`${this.apiUrl}/${Id}`, Task);
  }

  // Method To Delete Task
  DeleteTask(Id: number): Observable<void> {
    return this.Http.delete<void>(`${this.apiUrl}/${Id}`);
  }

  // Method To Mark Task As Completed
  MarkAsCompleted(Id: number): Observable<void> {
    return this.Http.patch<void>(`${this.apiUrl}/${Id}`, {});
  }
}
