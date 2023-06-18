//
//  Term.swift
//  schedule.ios.application
//
//  Created by Иван Светлов on 11.06.2023.
//

import Foundation

struct Term: Identifiable, Codable, Equatable, Hashable {
    var id: Int
    var courseTerm: Int
    var course: Course
}
