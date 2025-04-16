import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { DateFormatPipe } from './pipes/date-format.pipe';

@NgModule({
  declarations: [
    DateFormatPipe
  ],
  imports: [CommonModule],
})
export class CoreModule {}
