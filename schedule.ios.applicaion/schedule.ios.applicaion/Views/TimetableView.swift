//
//  TimetableView.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import SwiftUI

struct TimetableView: View {
    var timetable: Timetable
    
    var body: some View {
        VStack {
            Text(timetable.date.day.name)
                .font(.system(size: 32))
                .bold()
                .padding([.top, .leading, .trailing], 10)
            VStack {
                ForEach(timetable.lessons) { lesson in
                    LessonView(lesson: lesson)
                }
            }
            .padding([.bottom, .leading, .trailing])
        }
        .overlay(
            RoundedRectangle(cornerRadius: 12)
                .stroke(.blue, lineWidth: 3)
                .padding(.horizontal)
        )
    }
}

//#Preview {
//    TimetableView()
//}
