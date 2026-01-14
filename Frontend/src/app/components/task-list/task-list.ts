import { TaskService } from '../../core/services/task';
import { TaskItem } from './../../core/models/task';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-task-list',
  imports: [],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css',
})
export class TaskList implements OnInit {
  // This Array To Hold The List Of Tasks From The Backend
  Tasks: TaskItem[] = [];

  constructor(private taskService: TaskService) {
    taskService.getAllTasks().subscribe({
      next: (tasks) => {
        this.Tasks = tasks;
      },
    });
  }
  ngOnInit(): void {
    this.LoadTasks();
  }

  // Method To Load All Tasks From The Backend To The Tasks Array
  LoadTasks(): void {
    this.taskService.getAllTasks().subscribe({
      // On Success, Assign The Data To The Tasks Array
      next: (Data) => {
        this.Tasks = Data;
      },
      // On Error, Log The Error To The Console
      error: (Err) => {
        console.error('Error Loading Tasks', Err);
      },
    });
  }
  DeleteTask(Id: number): void {
    this.taskService.DeleteTask(Id).subscribe({
      // On Success, Refresh The Task List
      next: () => {
        this.LoadTasks();
      },
      // On Error, Log The Error To The Console
      error: (Err) => {
        console.error('Delete Operation Failed:', Err);
      },
    });
  }
}
