// This Interface To Match The Task Item Structure From The Backend
export interface TaskItem {
  id: number;
  title: string;
  description?: string;
  isCompleted: boolean;
  createdAt: Date;
}
