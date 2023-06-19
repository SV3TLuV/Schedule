//
//  LessonView.swift
//  schedule.ios.applicaion
//
//  Created by Иван Светлов on 18.06.2023.
//

import SwiftUI

struct LessonView: View {
    var lesson: Lesson
    
    private var borderColor: Color {
        return lesson.isChanged ? .red : .blue
    }
    
    var body: some View {
        VStack {
            HStack {
                Text("\(lesson.discipline?.name ?? "")\(lesson.subgroup != nil ? "\nПодгруппа: \(lesson.subgroup!)" : "")")
                    .frame(maxWidth: .infinity, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.horizontal)
                    )
            }
            .padding(.bottom, 5)
            HStack {
                Text(lesson.teacherClassrooms.at(0)?.teacher.getFio() ?? "")
                    .frame(maxWidth: .infinity, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.leading)
                    )
                Text(lesson.teacherClassrooms.at(0)?.classroom?.cabinet ?? "")
                    .frame(maxWidth: 120, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.trailing)
                    )
            }
            .padding(.bottom, 5)
            HStack {
                Text(lesson.teacherClassrooms.at(1)?.teacher.getFio() ?? "")
                    .frame(maxWidth: .infinity, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.leading)
                    )
                Text(lesson.teacherClassrooms.at(1)?.classroom?.cabinet ?? "")
                    .frame(maxWidth: 120, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.trailing)
                    )
            }
            .padding(.bottom, 5)
            HStack {
                Text(lesson.time?.getRange() ?? "")
                    .frame(maxWidth: .infinity, minHeight: 20)
                    .padding(.all, 5)
                    .overlay(
                        RoundedRectangle(cornerRadius: 20)
                            .stroke(borderColor, lineWidth: 1)
                            .padding(.horizontal)
                    )
            }
        }
        .frame(maxWidth: .infinity)
    }
}

//#Preview {
//    LessonView()
//}
