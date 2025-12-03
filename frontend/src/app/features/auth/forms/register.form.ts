import { FormControl, FormGroup, Validators } from '@angular/forms';

export const registerForm = new FormGroup({
  fullName: new FormControl('', [Validators.required]),
  userName: new FormControl('', [Validators.required]),
  password: new FormControl('', [Validators.required, Validators.minLength(8)]),
  jobId: new FormControl<number | null>(null),
  roleId: new FormControl(2, [Validators.required]) // I should delete it, it's only for testing purposes
});
