import { Component, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'client-app';
  text = signal('');

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.http
      .get('http://localhost:5000/api/hello')
      .subscribe((response: any) => {
        const typed = response as { res: string };
        this.text.set(typed.res);
      });
  }
}
