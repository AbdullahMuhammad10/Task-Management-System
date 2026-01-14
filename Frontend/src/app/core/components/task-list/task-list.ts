import { TaskService } from '../../services/task';
import { TaskItem } from '../../models/task';
import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-task-list',
  imports: [RouterLink, CommonModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css',
})
export class TaskList implements OnInit {
  // This Array To Hold The List Of Tasks From The Backend
  Tasks: TaskItem[] = [];

  constructor(private taskService: TaskService) {}
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
