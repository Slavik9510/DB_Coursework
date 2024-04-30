import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneNumber'
})
export class PhoneNumberPipe implements PipeTransform {
  transform(value: string): string {
    console.log('Original Value:', value);
    if (!value) return value;
    let formatted = value.replace(/\D/g, "");
    console.log('Digits Only:', formatted);
    if (formatted.length === 12) {
      let result = `+${formatted.slice(0, 2)} ${formatted.slice(2, 5)} ${formatted.slice(5, 7)}-${formatted.slice(7, 9)}-${formatted.slice(9)}`;
      console.log('Formatted Result:', result);
      return result;
    }
    return value;
  }

}
