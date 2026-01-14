import { Component } from '@angular/core';
import { AddTaskDto } from '../../models/add-task-dto';
import { TaskService } from '../../services/task';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-add-task',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './add-task.html',
  styleUrl: './add-task.css',
})
export class AddTask {
  TaskForm: FormGroup;

  constructor(private Fb: FormBuilder, private taskService: TaskService, private router: Router) {
    this.TaskForm = this.Fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
    });
  }

  OnSubmit(): void {
    if (this.TaskForm.valid) {
      const NewTask: AddTaskDto = this.TaskForm.value;
      this.taskService.CreateTask(NewTask).subscribe({
        next: () => {
          this.router.navigate(['/']); // Redirect To List After Success
        },
        error: (Err) => console.error('Error Creating Task:', Err),
      });
    }
  }
}
