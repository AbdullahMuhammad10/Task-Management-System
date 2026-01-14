// This Dto To Match The Data Required To Update A Task In The Backend
export interface UpdateTaskDto {
  title?: string;
  description?: string;
  isCompleted?: boolean;
}
