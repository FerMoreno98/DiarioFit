import { Component, Inject } from '@angular/core';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

export type ConfirmDialogData = {
  title: string;
  message: string;
  okText?: string;
  cancelText?: string;
};

@Component({
  selector: 'app-modal-confirmacion',
  standalone:true,
  imports: [MatDialogModule,MatButtonModule],
  templateUrl: './modal-confirmacion.html',
  styleUrl: './modal-confirmacion.css',
})
export class ModalConfirmacion {
  constructor(
    private dialogRef: MatDialogRef<ModalConfirmacion, boolean>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData
  ) {}

  close(result: boolean) {
    this.dialogRef.close(result);
  }
}
