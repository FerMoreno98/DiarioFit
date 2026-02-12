import { Component, Inject } from '@angular/core';
import {
  MatDialogModule,
  MatDialogRef,
  MAT_DIALOG_DATA,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

export type ErrorDialogData = {
  title: string;
  message: string;
  cancelText?: string;
};
@Component({
  selector: 'app-modal-error',
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './modal-error.html',
  styleUrl: './modal-error.css',
})
export class ModalError {
  constructor(
    private dialogRef: MatDialogRef<ModalError>,
    @Inject(MAT_DIALOG_DATA) public data: ErrorDialogData
  ) {}

  close(): void {
    this.dialogRef.close();
  }
}
