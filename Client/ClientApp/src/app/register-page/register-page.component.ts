import { AuthenticationService } from './../services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AlertService } from '../services/alert.service';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.css'],
})
export class RegisterPageComponent implements OnInit {
  public registerForm!: FormGroup;

  constructor(private authenticationService: AuthenticationService,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.registerForm = new FormGroup({
      username: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required),
      confirmPassword: new FormControl('', Validators.required),
      workForFood:  new FormControl(false, Validators.required),
    });
  }

  public onSubmit() {
    const passwordLocal = this.registerForm!.get('password')!.value;
    const confirmPasswordLocal = this.registerForm!.get('confirmPassword')!.value
    const workForFood = this.registerForm!.get('workForFood')!.value
    if (!workForFood) {
      this.alertService.openSnackBar('You have to work for food here!!!');
      return;
    }
    if (passwordLocal != confirmPasswordLocal) {
      this.alertService.openSnackBar('Password confirmation is incorrect');
      return;
    }
    this.authenticationService.register(
        this.registerForm.get('email')!.value,
        passwordLocal
      );
  }
}
