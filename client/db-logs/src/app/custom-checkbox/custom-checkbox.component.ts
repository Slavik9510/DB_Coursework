import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-custom-checkbox',
  templateUrl: './custom-checkbox.component.html',
  styleUrls: ['./custom-checkbox.component.css']
})
export class CustomCheckboxComponent {
  @Input() label: string | undefined;
  isChecked: boolean = false;

  toggle() {
    this.isChecked = !this.isChecked;
  }
}
