import { Component } from '@angular/core';
import { TaskService } from '../../services/task';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-update-task',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './update-task.html',
  styleUrl: './update-task.css',
})
export class UpdateTask {
  UpdateForm: FormGroup;
  TaskId!: number; // We Will Get This From URL

  constructor(
    private Fb: FormBuilder,
    private Route: ActivatedRoute, // To Read Route Parameters
    private router: Router,
    private taskService: TaskService
  ) {
    this.UpdateForm = this.Fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      isCompleted: [false],
    });
  }

  ngOnInit(): void {
    // To Get The Task ID From The URL
    this.TaskId = Number(this.Route.snapshot.paramMap.get('id'));

    // Load The Existing Task Data
    this.taskService.getTaskById(this.TaskId).subscribe({
      next: (Task) => {
        this.UpdateForm.patchValue(Task); // Automatic Mapping From Object To Form
      },
      error: (Err) => console.error('Failed To Load Task Data:', Err),
    });
  }

  OnSubmit(): void {
    if (this.UpdateForm.valid) {
      this.taskService.UpdateTask(this.TaskId, this.UpdateForm.value).subscribe({
        next: () => {
          this.router.navigate(['/tasks']); // Redirect Back After Success
        },
        // Error Logging
        error: (Err) => console.error('Update Failed:', Err),
      });
    }
  }
}
