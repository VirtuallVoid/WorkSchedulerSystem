export interface ApiResponse<T> {
  succeeded: boolean;
  message: string | null;
  error: string | null;
  code: number;
  data: T;
  validationErrors?: any;
}
